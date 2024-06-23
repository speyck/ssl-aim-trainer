#pragma once

// lib
#include "IProcess.h"

// win
#include <windows.h>

class WinProcess : public IProcess 
{
private:
    QString processName;
    DWORD processID;
    HANDLE processHandle;
	quintptr baseAddress;

    bool get_process_id_init();
	bool get_base_address_init();

public:
    explicit WinProcess(const QString& name);
    virtual ~WinProcess();

    bool is_running() const override;
    QString get_name() const override;
    quintptr get_base_address() const override;
    bool read_memory(quintptr address, QByteArray buffer) const override;
};

