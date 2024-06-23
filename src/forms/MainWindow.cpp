// lib
#include "forms/MainWindow.h"
#ifdef WIN32
#include "process/WinProcess.h"
#define CreateProcess new WinProcess
#endif

// qt
#include <QDebug>

const char* processName = "ShellShockLive.exe";

MainWindow::MainWindow() 
{
	process = CreateProcess(processName);

	QString status = process->is_running() ? "Running" : "Not Running";
	QString text = QString("%1-Status: %2").arg(processName, status);
	QLabel* statusLabel = new QLabel(text, this);
	statusLabel->setGeometry(10, 10, 200, 30);

    // Set the title and size of the main window
    setWindowTitle("Main Window");
    resize(400, 300);
}

MainWindow::~MainWindow()
{
	delete process;
}

