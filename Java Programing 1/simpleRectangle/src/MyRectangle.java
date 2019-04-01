public class MyRectangle {

    double height, width;

    MyRectangle()
    {
        height = 1.0;
        width = 1.0;
    }

    MyRectangle(double newWidth, double newHeight)
    {
        height = newHeight;
        width = newWidth;
    }

    double getArea()
    {
        return width*height;
    }

    double getPerimeter()
    {
        return (2*width)+(2*height);
    }


}
