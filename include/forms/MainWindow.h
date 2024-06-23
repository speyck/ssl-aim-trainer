#pragma once

// qt
#include <QMainWindow>
#include <QLabel>

// lib
#include "process/IProcess.h"

class MainWindow : public QMainWindow 
{
    Q_OBJECT

	IProcess* process;

public:
    MainWindow();
	~MainWindow();
};

