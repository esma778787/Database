using System;
using System.Collections.Generic;

public class DatabaseTable
{
    private List<List<object>> table;

    public DatabaseTable(List<List<object>> table)
    {
        this.table = table;
    }

    public DatabaseResult Select(params int[] columns)
    {
        List<List<object>> resultData = new List<List<object>>();

        foreach (var row in table)
        {
            List<object> selectedColumns = new List<object>();
            foreach (var colIndex in columns)
            {
                selectedColumns.Add(row[colIndex]);
            }
            resultData.Add(selectedColumns);
        }

        return new DatabaseResult(resultData);
    }

    public DatabaseResult SelectAll()
    {
        return new DatabaseResult(table);
    }
}

public class DatabaseResult
{
    private List<List<object>> data;

    public DatabaseResult(List<List<object>> data)
    {
        this.data = data;
    }

    public object this[int row, int col]
    {
        get { return data[row][col]; }
    }

    public int RowCount
    {
        get { return data.Count; }
    }

    public int ColumnCount
    {
        get { return data.Count > 0 ? data[0].Count : 0; }
    }

    public void PrintColumnType(int col)
    {
        if (col >= 0 && col < ColumnCount)
        {
            Type type = data[0][col].GetType();
            Console.WriteLine($"Column {col + 1} Type: {type.Name}");
        }
    }
}

class Program
{
    static void Main()
    {
        List<List<object>> sampleTable = new List<List<object>>
        {
            new List<object> { 1, "Ali", 25, DateTime.Parse("1995-05-10") },
            new List<object> { 2, "Halime", "Engineer", DateTime.Parse("1990-08-15") },
            new List<object> { 3, "Hüseyin", true, "Marketing", DateTime.Parse("1987-02-20") },
            new List<object> { 4, "Habibe", 18, DateTime.Parse("2005-04-12") },
            new List<object> { 5, "Esma", 25, DateTime.Parse("2002-03-27") },
            new List<object> { 6, "İrem", "Computer Engineer", DateTime.Parse("2000-03-17") },
            new List<object> { 7, "Elif", true, "Marketing", DateTime.Parse("1999-06-10") },
            new List<object> { 8, "Hasibe", 10, DateTime.Parse("2014-01-22") },
            new List<object> { 9, "Emine", 36, DateTime.Parse("1986-11-11") },
            new List<object> { 10, "Eyüp", 46, DateTime.Parse("1976-04-01") }
        };

        DatabaseTable sampleDatabaseTable = new DatabaseTable(sampleTable);

        Console.WriteLine("Select Sutun1, Sutun2, Sutun3 from Table:");
        DatabaseResult result1 = sampleDatabaseTable.Select(0, 1, 2);
        PrintResult(result1);

        Console.WriteLine("\nSelect * from Table:");
        DatabaseResult result2 = sampleDatabaseTable.SelectAll();
        PrintResult(result2);
    }

    static void PrintResult(DatabaseResult result)
    {
        Console.WriteLine($"Row Count: {result.RowCount}, Column Count: {result.ColumnCount}");
        for (int i = 0; i < result.RowCount; i++)
        {
            for (int j = 0; j < result.ColumnCount; j++)
            {
                Console.Write(result[i, j] + "\t");
                result.PrintColumnType(j);
            }
            Console.WriteLine();
            Console.ReadLine();
        }
        Console.WriteLine();
        Console.ReadLine();
    }
}

