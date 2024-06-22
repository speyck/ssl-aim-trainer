// lib
#include "forms/MainWindow.h"
#include "process/check_process.h"

// qt
#include <QDebug>

const char* processName = "ShellShockLive.exe";

MainWindow::MainWindow() 
{
	QString status = is_process_running(processName) ? "Running" : "Not Running";
	QString text = QString("%1-Status: %2").arg(processName, status);
	QLabel* statusLabel = new QLabel(text, this);
	statusLabel->setGeometry(10, 10, 200, 30);

    // Set the title and size of the main window
    setWindowTitle("Main Window");
    resize(400, 300);
}

