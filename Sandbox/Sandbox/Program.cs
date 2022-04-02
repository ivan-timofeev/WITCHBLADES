// See https://aka.ms/new-console-template for more information
using System.Diagnostics.CodeAnalysis;

Console.WriteLine("Hello, World!");

var test = new Test();
Console.WriteLine("KEK");




class Test
{
    [NotNull]
    public string Shit { get; set; }
}