// lib
#include "process/WinProcess.h"

// win
#include <tlhelp32.h>

// qt
#include <QByteArray>
#include <QDebug>

WinProcess::WinProcess(const QString& name) 
	: processName(name), processID(0), processHandle(nullptr), baseAddress(0)
{
	if (!processName.endsWith(".exe")) 
		processName += ".exe";

	if (get_process_id_init()) 
		processHandle = OpenProcess(PROCESS_VM_READ, FALSE, processID);

	if (!get_base_address_init())
		qDebug() << "Failed to get base address of " << processName;
}

WinProcess::~WinProcess()
{
	if (processHandle) 
		CloseHandle(processHandle);
}

bool WinProcess::get_process_id_init()
{
	HANDLE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
	if (snapshot == INVALID_HANDLE_VALUE) 
		return false;

	PROCESSENTRY32 processEntry{};
	processEntry.dwSize = sizeof(PROCESSENTRY32);

	if (Process32First(snapshot, &processEntry)) 
	{
		do 
		{
			if (processName != QString::fromWCharArray(processEntry.szExeFile))
				continue;

			processID = processEntry.th32ProcessID;
			CloseHandle(snapshot);
			return true;
		} while (Process32Next(snapshot, &processEntry));
	}

	CloseHandle(snapshot);

	return false;
}

bool WinProcess::get_base_address_init()
{
    MODULEENTRY32 moduleEntry;
    moduleEntry.dwSize = sizeof(MODULEENTRY32);
    HANDLE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPMODULE | TH32CS_SNAPMODULE32, processID);

    if (Module32First(snapshot, &moduleEntry)) 
	{
        do 
		{
            if (processName == QString::fromWCharArray(moduleEntry.szModule)) 
			{
                CloseHandle(snapshot);
                baseAddress = (quintptr)moduleEntry.modBaseAddr;
                return true;
            }
        } while (Module32Next(snapshot, &moduleEntry));
    }

    CloseHandle(snapshot);
    return false;
}

bool WinProcess::is_running() const
{
	return processHandle != nullptr;
}

QString WinProcess::get_name() const
{
	return processName;
}

quintptr WinProcess::get_base_address() const
{
	return baseAddress;
}

bool WinProcess::read_memory(quintptr offset, QByteArray buffer) const
{
	if (!is_running()) 
		return false;

    quintptr baseAddress = get_base_address();
    if (baseAddress == 0) 
        return false;

    quintptr readAddress = baseAddress + offset;

	qsizetype size = buffer.size();

	SIZE_T bytesRead;
	return ReadProcessMemory(processHandle, (LPCVOID)offset, buffer.data(), size, &bytesRead);
}
