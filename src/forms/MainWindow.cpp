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
	process = std::unique_ptr<IProcess>(CreateProcess(processName));

	redraw();

    // Set the title and size of the main window
    setWindowTitle("Main Window");
    resize(400, 300);
}

void MainWindow::redraw() 
{
	QString status = process->is_running() ? "Running" : "Not Running";
	QString text = QString("%1-Status: %2").arg(processName, status);
	lblStatus = new QLabel(text, this);
	lblStatus->setGeometry(10, 10, 250, 30);

	btnRefresh = new QPushButton("Refresh", this);
	btnRefresh->setGeometry(10, 50, 100, 30);
	connect(btnRefresh, &QPushButton::clicked, this, &MainWindow::btnRefresh_Clicked);

	lblPlayerPosY = new QLabel("Player Pos Y: ", this);
	lblPlayerPosX = new QLabel("Player Pos X: ", this);

	lblPlayerPosY->setGeometry(10, 90, 250, 30);
	lblPlayerPosX->setGeometry(10, 130, 250, 30);
}

void MainWindow::btnRefresh_Clicked()
{
	lblPlayerPosX->setText(QString("Player Pos X: %1").arg("-"));
	lblPlayerPosY->setText(QString("Player Pos Y: %1").arg("-"));
}

MainWindow::~MainWindow()
{ 
	delete lblStatus;
	delete btnRefresh;
	delete lblPlayerPosY;
	delete lblPlayerPosX;
}

