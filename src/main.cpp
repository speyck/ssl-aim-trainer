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

	MainWindow window;
	window.show();
    
    return app.exec();
}

