public class Rectangle {

    public static void main(String[] args) {

        MyRectangle rectangle1 = new MyRectangle();
        System.out.println("The area of the area of a rectangle with width " + rectangle1.width + " and height "
                + rectangle1.height + " is " +rectangle1.getArea());
        System.out.println("The perimeter of this rectangle is " + rectangle1.getPerimeter());

        MyRectangle rectangle2 = new MyRectangle(4.0, 40.0);
        System.out.println("The area of the area of a rectangle with width " + rectangle2.width + " and height "
                + rectangle2.height + " is " +rectangle2.getArea());
        System.out.println("The perimeter of this rectangle is " + rectangle2.getPerimeter());

        MyRectangle rectangle3 = new MyRectangle(3.5, 35.9);
        System.out.println("The area of the area of a rectangle with width " + rectangle3.width + " and height "
                + rectangle3.height + " is " +rectangle3.getArea());
        System.out.println("The perimeter of this rectangle is " + rectangle3.getPerimeter());
    }
}
