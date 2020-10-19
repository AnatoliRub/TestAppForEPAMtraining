using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ShapeLib;

namespace testEpamApp
{
    class Program
    {
        static void Main()
        {
            List<Shape> shapeTriangle = new List<Shape>();
            List<Shape> shapeCircle = new List<Shape>();
            List<Shape> shapeRectangle = new List<Shape>();

            var stringsReader = ReadDataFromFile();
            
            List<double[]> parametersTriangle = new List<double[]>();
            List<double[]> parametersCircle = new List<double[]>();
            List<double[]> parametersRectangle = new List<double[]>();
            
            string patternRectangle = @"Rectangle(\s+\d+.\d+|\s+\d+){2}";
            string patternTriangle = @"Triangle(\s+\d+.\d+|\s+\d+){3}";
            string patternCircle = @"Circle(\s+\d+.\d+|\s+\d+){1}";


            ParseData(stringsReader, patternTriangle, parametersTriangle);
            ParseData(stringsReader, patternCircle, parametersCircle);
            ParseData(stringsReader, patternRectangle, parametersRectangle);
            Console.WriteLine();
            

            CreateShape(parametersTriangle, shapeTriangle);
            CreateShape(parametersCircle, shapeCircle);
            CreateShape(parametersRectangle, shapeRectangle);

            List<Shape> listShapeWithMaxArea = new List<Shape>();
            Dictionary<double, string> listShapeWithMaxCommonPerimeter = new Dictionary<double, string>();

            Console.WriteLine("1. Calculate the average perimeter and area of all figures.");
            Calculate(shapeRectangle, listShapeWithMaxArea, listShapeWithMaxCommonPerimeter);
            Calculate(shapeCircle, listShapeWithMaxArea, listShapeWithMaxCommonPerimeter);
            Calculate(shapeTriangle, listShapeWithMaxArea, listShapeWithMaxCommonPerimeter);

            Output(listShapeWithMaxArea, listShapeWithMaxCommonPerimeter);

            Console.WriteLine("Hello World!");
        }

        static void Output(List<Shape> listShape, Dictionary<double, string> typeShapeWithMaxCommonPerimeter)
        {
            Shape shapeWithMaxArea = listShape[0];
            string type = null;
            double maxCommonPerimeterValue = 0.0;
            foreach (var item in listShape)
            {
                if(shapeWithMaxArea.Area() <= item.Area())
                {
                    shapeWithMaxArea = item;
                }
            }

            foreach (var item in typeShapeWithMaxCommonPerimeter)
            {
                if(item.Key > maxCommonPerimeterValue)
                {
                    maxCommonPerimeterValue = item.Key;
                    type = item.Value;
                }
            }
            Console.WriteLine("<------------------------>");
            Console.WriteLine("2. Find the shape with the largest area.");
            Console.WriteLine($"Type of Shape has max common perimeter is {type}.");
            Console.WriteLine("3. Find the type of shape with the largest value average perimeter among all other types of shapes.");
            Console.WriteLine($"{shapeWithMaxArea.ToString()} has maxArea.");
        }

        static void Calculate(List<Shape> shape, List<Shape> listShapeWithMaxArea, Dictionary<double, string> shapesWithMaxCommonPerimeter)
        {
            var commonArea = 0.0;
            var commonPerimeter = 0.0;

            var maxArea = 0.0;
            Shape shapeWithMaxArea = null;
            string typeShape = shape[0].GetType().ToString().Split('.')[1];
            Console.WriteLine($"<--------{typeShape}-------->");
            foreach (var item in shape)
            {
                if(item.Area()> maxArea)
                {
                    maxArea = item.Area(); 
                    shapeWithMaxArea = item;
                }
                Console.WriteLine($"{item.ToString()} - Area: {String.Format("{0:f2}", item.Area())}; Perimeter: {String.Format("{0:f2}", item.Perimeter())};");
                commonArea += item.Area();
                commonPerimeter += item.Perimeter();
            }
            listShapeWithMaxArea.Add(shapeWithMaxArea);
            shapesWithMaxCommonPerimeter.Add(commonPerimeter / shape.Count, typeShape);

            Console.WriteLine($"Average area of {typeShape}: {String.Format("{0:f2}", commonArea / shape.Count)}");
            Console.WriteLine($"Common area of {typeShape}: {String.Format("{0:f2}", commonArea)}");
            Console.WriteLine($"Average perimeter of {typeShape}: {String.Format("{0:f2}", commonPerimeter / shape.Count)}");
            Console.WriteLine($"Common perimeter of {typeShape}: {String.Format("{0:f2}", commonPerimeter)}");
        }

        static void CreateShape(List<double[]> parameters, List<Shape> shape)
        {
            foreach (var item in parameters)
            {
                switch (item.Length)
                {
                    case 1:
                        shape.Add(new Circle(item));
                        break;
                    case 2:
                        shape.Add(new Rectangle(item));
                        break;
                    case 3:
                        shape.Add(new Triangle(item));
                        break;
                    default:
                        break;
                }    
            }
        }

        static void ParseData(string item, string pattern, List<double[]> parameters)
        {
            Match match = Regex.Match(item, pattern, RegexOptions.IgnoreCase);
            while (match.Success)
            {
                var parameterString = match.Value.ToUpper();
                var patternSub = @"\s+";
                var str = Regex.Split(parameterString, patternSub).ToList<string>();
                str.RemoveAt(0);
                double[] variables = new double[str.Count];
                var i = 0;
                foreach (var items in str)
                {
                    variables[i] = Double.Parse(items);
                    i++;
                }
                parameters.Add(variables);
                match = match.NextMatch();
            }
        }

        static string ReadDataFromFile()
        {
            using var stream = new StreamReader("info.txt");
            return stream.ReadToEnd();
        }
    }
}
