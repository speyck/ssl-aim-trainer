// lib
#include "forms/MainWindow.h"

// qt
#include <QDebug>

MainWindow::MainWindow() 
{
	qDebug() << "Hello World!";

    // Create the label and set its text
    label = new QLabel("Hello, Qt!", this);
    // Optionally, set the geometry of the label
    label->setGeometry(10, 10, 200, 30);

    // Set the title and size of the main window
    setWindowTitle("Main Window");
    resize(800, 600);
}

