using System;
using Xunit;
using ShapeLib;

namespace TestEpamApp.Tests
{
    public class ShapeLibTest
    {
        [Fact]
        public void Perimeter_Rectangle_6and8_return28()
        {
            double[] mass = new double[] { 6.0, 8.0 };
            double expected = 28.0;
            Shape rectangle = new Rectangle(mass);
            double actual = rectangle.Perimeter();
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Area_Rectangle_6and8_return48()
        {
            double[] mass = new double[] { 6.0, 8.0 };
            double expected = 48.0;
            Shape rectangle = new Rectangle(mass);
            double actual = rectangle.Area();
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Perimeter_Triangle_3and4and5_return6()
        {
            double[] mass = new double[] { 3.0, 4.0, 5.0 };
            double expected = 12.0;
            Shape triangle = new Triangle(mass);
            double actual = triangle.Perimeter();
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Area_Triangle_3and4and5_return6()
        {
            double[] mass = new double[] { 3.0, 4.0, 5.0 };
            double expected = 6.0;
            Shape triangle = new Triangle(mass);
            double actual = triangle.Area();
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Perimeter_Circle_2_return12point57()
        {
            double[] mass = new double[] { 2.0 };
            double expected = 12.57;
            Shape circle = new Circle(mass);
            double actual = Double.Parse(String.Format("{0:f2}", circle.Perimeter()));
            Console.WriteLine(actual);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Area_Circle_2_return12point57()
        {
            double[] mass = new double[] { 2.0 };
            double expected = 12.57;
            Shape circle = new Circle(mass);
            double actual = Double.Parse(String.Format("{0:f2}", circle.Area()));
            Assert.Equal(expected, actual);
        }
    }
}
