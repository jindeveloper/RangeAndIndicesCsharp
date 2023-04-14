using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace TestProjectRangeCsharp;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;
    
    public UnitTest1(ITestOutputHelper output)
    {
        this._output = output;
    }

    private readonly string[] _carCollections = new string[] { "Mazda", "Toyota", "Suzuki", "BMW", "Honda", "Ford" };

    [Fact]
    public void Test_Indices_That_Counts_From_Start()
    {
        var index = new Index(value: 2);
        var result = _carCollections[index];
        Assert.Equal("Suzuki", result);
    }

    [Fact]
    public void Test_Indices_That_Counts_From_Start_Using_Implicit_Conversion()
    {
        Index index = 2;
        var result = _carCollections[index];
        Assert.Equal("Suzuki", result);
    }

    [Fact]
    public void Test_Indices_That_Counts_From_The_End()
    {
        var index = new Index(value: 2, fromEnd: true);
        var result = _carCollections[index];
        Assert.Equal("Honda", result);
    }
    
    [Fact]
    public void Test_Indices_That_Counts_From_The_End_Using_Caret_Operator()
    {
        var index = ^2;
        var result = _carCollections[index];
        Assert.Equal("Honda", result);
    }

    [Fact]
    public void Test_Indices_Throw_IndexOUtOfRangeException()
    {
        var index = ^0;
        Assert.Throws<IndexOutOfRangeException>(() => _carCollections[index]);
    }
    
    [Fact]
    public void Test_FullRange()
    {
        var result = new StringBuilder();
        
        //full range [..]
        foreach (var car in _carCollections[..])
        {
            var output = string.Concat(car, Environment.NewLine);
            this._output.WriteLine(output);
            result.Append(output);
        }

        var expected = $"{string.Concat("Mazda", Environment.NewLine)}" +
                          $"{string.Concat("Toyota", Environment.NewLine)}" +
                          $"{string.Concat("Suzuki", Environment.NewLine)}" +
                          $"{string.Concat("BMW", Environment.NewLine)}" +
                          $"{string.Concat("Honda", Environment.NewLine)}" +
                          $"{string.Concat("Ford", Environment.NewLine)}";

        Assert.Equal(expected, result.ToString());
    }

    [Fact]
    public void Test_Access_Start_From_The_3rd_Index()
    {
        var selectedCarsOnly = new List<string>();
        //open-ended range declaration
        foreach (var car in _carCollections[3..]) 
        {
            this._output.WriteLine(car);
            selectedCarsOnly.Add(car);
        }
        
        Assert.True(selectedCarsOnly.Count == 3);
      
        Assert.Equal("BMW", selectedCarsOnly[0]);
        Assert.Equal("Honda", selectedCarsOnly[1]);
        Assert.Equal("Ford", selectedCarsOnly[2]);
    }
    
    [Fact]
    public void Test_Access_First_Three_Elements()
    {
        var selectedCarsOnly = new List<string>();
        
        //start at index zero and end at the 3rd element
        foreach (var car in _carCollections[0..3]) 
        {
            this._output.WriteLine(car);
            selectedCarsOnly.Add(car);
        }
        
        Assert.True(selectedCarsOnly.Count == 3);
        Assert.Equal("Mazda",selectedCarsOnly[0]);
        Assert.Equal("Toyota", selectedCarsOnly[1]);
        Assert.Equal("Suzuki", selectedCarsOnly[2]);
    }
    
    [Fact]
    public void Test_Defining_Ranges_Outside_Brackets()
    {
        var range = new Range(start: 1, end: 4);
        
        var selectedCarsOnly = new List<string>();

        foreach (var car in _carCollections[range]) 
        {
            this._output.WriteLine(car);
            selectedCarsOnly.Add(car);
        }
        
        Assert.True(selectedCarsOnly.Count == 3);
      
        Assert.Equal("Toyota", selectedCarsOnly[0]);
        Assert.Equal("Suzuki", selectedCarsOnly[1]);
        Assert.Equal("BMW", selectedCarsOnly[2]);
    }

    [Fact]
    public void Test_Range_With_Hat_Character()
    {
        
        // two ways to define the same index, 2 in from the start
        Index x = new(value: 2); // counts from the start
        Index y = 2; // using implicit int conversion operator
        
        // two ways to define the same index, 1 in from the end
        Index a = new(value: 1, fromEnd: true);
        Index b = ^1; // using the caret operator
        
        
        var range = Range.StartAt(^3);
        
        var selectedCarsOnly = new List<string>();

        foreach (var car in _carCollections[range]) 
        {
            this._output.WriteLine(car);
            selectedCarsOnly.Add(car);
        }
        
        Assert.True(selectedCarsOnly.Count == 3);
      
        Assert.Equal("BMW", selectedCarsOnly[0]);
        Assert.Equal("Honda", selectedCarsOnly[1]);
        Assert.Equal("Ford", selectedCarsOnly[2]);

    }
}