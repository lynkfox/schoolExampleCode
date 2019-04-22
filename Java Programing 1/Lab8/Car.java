public class Car {

    // Early lab - variables intentionally left public, private is next week

    int yearModel, speed;
    String make;

    Car(String carMake, int carYear)
    {
        yearModel = carYear;
        make = carMake;
        speed = 0;
    }

    int getYearModel()
    {
        return yearModel;
    }
    String getMake()
    {
        return make;
    }
    int getSpeed()
    {
        return speed;
    }
    void accelerate()
    {
        speed += 5;
    }

    void brake()
    {
        speed -=5;
    }


}
