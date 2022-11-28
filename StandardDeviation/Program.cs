using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace StandardDeviation
{
    class Program
    {
        static void Main(string[] args)
        {
          
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(@"Z:\analysis\falle\alt");
            foreach (string fileName in fileEntries)

            {
                using (var dataFile = new StreamReader(fileName))
                {

                    string FileName = dataFile.ReadLine(); /*< Line 1 filename, attention, CR at the end is also saved!*/
                    dataFile.ReadLine(); /*< Line 2 sampling rate (not read in, fixed!)*/
                    var SamplingRate = 5000;
                    var TestText = dataFile.ReadLine(); /*< row 3 Testtext*/
                    string vRefStr = dataFile.ReadLine().Substring(24); /*< row VRef. Parse VRef from the 24th index of the row*/

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
                    double d = double.Parse(vRefStr, Thread.CurrentThread.CurrentCulture.NumberFormat);

                    var ReferenceVoltage = d; //Convert.ToDouble(vRefStr);
                    List<double> normalisedValues = new List<double>();
                    List<double> voltages = new List<double>();
                    while (!dataFile.EndOfStream)
                    {
                        voltages.Add(Convert.ToDouble(dataFile.ReadLine()));
                    }
                    
                    //Averages
                    var average = voltages.Average();
                    Console.WriteLine($"Aveargae {average}");
                    //sum
                    double sumOfSquaresOfDifferences = voltages.Select(val => (val - average) * (val - average)).Sum();
                    Console.WriteLine($"sumOfSquaresOfDifferences {sumOfSquaresOfDifferences}");
                    //standard Deviation
                    double sd = Math.Sqrt(sumOfSquaresOfDifferences / voltages.Count);
                    Console.WriteLine($"standard Deviation {sd}");

                    foreach (var item in voltages)
                    {
                        var normalizedData = (item - average) / sd;
                        normalisedValues.Add(normalizedData);
                    }
                   

                    // check for the maximum value
                    double maximumValue = voltages.Max();

                    var valuesAboveMaximumValue = voltages.Where(x => x >= (0.9 * maximumValue)).Count();
                    if (valuesAboveMaximumValue >= 500)
                    {
                        //This is opening
                        Console.WriteLine("This is opening");
                    }
                    if (valuesAboveMaximumValue <= 100)
                    {
                        //this is closing
                        Console.WriteLine("This is closing");
                    }


                    List<double?> leads = new List<double?>();
                    List<double?> lags = new List<double?>();

                    for (int i = 0; i < voltages.Count; i++)
                    {
                        //when at pos 2
                        //lead should be obj1
                        double? lead = i == 0 ? null : voltages[i - 1];
                        leads.Add(lead);
                        //Objects before object at i
                        //Assumption we have ordered by age
                        //new list add everything b4 i
                        //lag should be obj3
                        double? lag = i == voltages.Count - 1 ? null : voltages[i + 1];
                        lags.Add(lag);
                    }
                }

            }

          
                
        }

    }
}
