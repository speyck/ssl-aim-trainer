#pragma once

// qt
#include <QString>
#include <QByteArray>
#include <QtGlobal>

class IProcess
{
public:
	virtual ~IProcess() = default;

	virtual bool is_running() const = 0;
	virtual QString get_name() const = 0;
	virtual quintptr get_base_address() const = 0;
	virtual bool read_memory(quintptr offset, QByteArray buffer) const = 0;
};

