# Shoenfield Machine
Shoenfield Machine is a simple model introduced by Joseph R. Shoenfield in his book 'Recursion Theory'. It is described by the following principles:
- it's as simple as a computing machine can be
- it has infinitely much memory
- it never makes a mistake

# Building and running
Project was developed in C# using .NET 10. Learn how to install .NET 10 on your system: https://dotnet.microsoft.com/en-us/download/dotnet/10.0.

Source code is placed in `src` directory.

Run `dotnet build` in solution directory to compile.
Run `dotnet run` in desired project directory to run.

# Specification
Shoenfield Machine consists of the following elements:
- registers
- program holder
- program counter

## Registers
All program memory is stored in the registers. For each number *i*, the machine has a register *R_i*. At each moment, *R_i* contains a number, which may change during computation.

## Program holder
Program holder contains a program, i.e. a finite sequence of instructions, numbered from 0 to *N-1*, where *N* is the number of instructions.

## Program counter
Program counter is a variable that contains a number of the current instruction.

## Machine usage
Usage of the machine requires inserting a program into the program holder and setting the desired starting values of the registers. Upon start, a value of *0* is inserted into the program counter, causing first instruction to be executed. At each step, the machine executes the instruction corresponding to the program counter value, if such instruction exists. If at any time the progam counter value is greater than number of instructions in the program, the machine halts. Note, that is is possible for the machine to run indefinitely.

## Instruction set
The machine only supports 3 instructions:
- `INCREASE R_i` - increases number stored in the *R_i* register by 1, and increases the program counter by 1
- `DECREASE R_i, n` - if number stored in the *R_i* register is *0*, increases the program counter by 1; otherwise decreases the number stored in the *R_i* register and sets the program counter to *n*
- `GO TO n` - sets the program counter to *n*

# Example programs
Note: program code includes line numbers to improve readability. Those need to be removed prior feeding code to the machine.

## Add
Inputs: R0, R1

Output: R0 = R0 + R1

Code:
```
0. DECREASE 1, 2
1. GO TO 4
2. INCREASE 0
3. GO TO 0
```

## Subtract
Inputs: R0, R1

Output: R0 = R0 - R1 iff R0 >= R1

Code:
```
0. DECREASE 1, 2
1. GO TO 5
2. DECREASE 0, 0
3. GO TO 4
4. GO TO 3
```

## Equal
Inputs: R0, R1

Output: R0 = 1 if R0 == R1, else 0

Code:
```
0. DECREASE 0, 4
1. DECREASE 1, 8
2. INCREASE 0
3. GO TO 8
4. DECREASE 1, 0
5. DECREASE 0, 7
6. GO TO 8
7. GO TO 5
```

# References
J. R. Shoenfield, Recursion Theory, Springer-Verlag, Berlin, 1993
