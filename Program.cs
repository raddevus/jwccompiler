// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

const char TAB = '\t';
char Look;

void GetChar(){
    Look = Convert.ToChar(Console.Read());
}

void Error(string s) {
    Console.WriteLine();
    // Original code has ^G which is bell char for beep sound
    // but that won't work here. 
    Console.WriteLine($"Error: {s}");
}

void Abort(string s){
    Error(s);
    Environment.Exit(1);
}

void Expected(string s){
    Abort($"{s} expected.");
}

void Match(char x){
    if (Look == x){
        GetChar();
        return;
    }
    Expected($"''{x}''");
}

bool IsAlpha(char c){
    if (char.IsLetter(c)){
        return true;
    }
    return false;
}

bool IsDigit(char c){
    if (char.IsDigit(c)){
        return true;
    }
    return false;
}

char GetNum(){
    if (!IsDigit(Look)){
        Expected("Integer");
    }
    GetChar();
    return Look;
}

void Emit(string s){
    Console.Write($"{TAB}s");
}

void EmitLn(string s){
    Emit($"{s}{Environment.NewLine}");
}

void Init(){
    GetChar();
}

// Starts the program
Init();




