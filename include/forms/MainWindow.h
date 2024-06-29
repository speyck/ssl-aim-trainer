#pragma once

// qt
#include <QMainWindow>
#include <QLabel>
#include <QPushButton>

// lib
#include "process/IProcess.h"

class MainWindow : public QMainWindow 
{
    Q_OBJECT

	std::unique_ptr<IProcess> process;
	QLabel* lblStatus = nullptr;
	QPushButton* btnRefresh = nullptr;
	QLabel* lblPlayerPosY = nullptr;
	QLabel* lblPlayerPosX = nullptr;

	void redraw();

private slots:
	void btnRefresh_Clicked();

public:
    MainWindow();
	~MainWindow();
};

