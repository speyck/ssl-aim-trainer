// qt
#include <QApplication>
#include <QWidget>
#include <QDebug>

// std
#include <iostream>

// lib
#include "forms/MainWindow.h"

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);

	// TODO : find out where tf this output goes
	std::cout << "Hello World!" << std::endl;
	qDebug() << "Hello World!";

	MainWindow window;
	window.show();
    
    return app.exec();
}

