#include "process/check_process.h"

// win
#include <windows.h>
#include <tlhelp32.h>
#include <tchar.h>
#include <psapi.h>
#include <iostream>

bool is_process_running(std::string_view name) 
{
	std::wstring word(name.begin(), name.end());
	TCHAR* processName = word.data();

	DWORD processIDs[1024], bytesReturned;

	// get the list of process ids
	if (!EnumProcesses(processIDs, sizeof(processIDs), &bytesReturned)) 
		return false;

	// number of processes
	int numProcesses = bytesReturned / sizeof(DWORD);

	for (int i = 0; i < numProcesses; i++) 
	{
		if (processIDs[i] == 0)
			continue;

		TCHAR processNameBuffer[MAX_PATH] = TEXT("<unknown>");
		HANDLE hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, FALSE, processIDs[i]);

		if (hProcess != NULL)
		{
			HMODULE hMod;
			DWORD cbNeeded;

			if (EnumProcessModules(hProcess, &hMod, sizeof(hMod), &cbNeeded))
			{
				GetModuleBaseName(hProcess, hMod, processNameBuffer, sizeof(processNameBuffer) / sizeof(TCHAR));
			}
		}

		if (_tcsicmp(processNameBuffer, processName) == 0)
		{
			CloseHandle(hProcess);
			return true; // Process found
		}

		if (hProcess != NULL)
			CloseHandle(hProcess);
	}

	return false; // Process not found
}

