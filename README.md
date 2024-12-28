## From Jack W. Crenshaw's Online Document
You can read more about the original at: [https://compilers.iecc.com/crenshaw/](https://compilers.iecc.com/crenshaw/)

## Background
I stumbled upon this reference from a X.com post and found it interesting and instantly wanted to translate Crenshaw's code from his original Turbo Pascal to C#.

## Branches = Steps
I will create branches named StepN (where N is a incremented value).
### Step1
Step1 is complete and contains the (converted) code as shown in the first write-up at: [https://compilers.iecc.com/crenshaw/tutor1.txt](https://compilers.iecc.com/crenshaw/tutor1.txt)

### Step 2
I've made the first changes that are noted at: [https://compilers.iecc.com/crenshaw/tutor2.txt](https://compilers.iecc.com/crenshaw/tutor2.txt)
Basically just added the `Expression` method.

The code does run and now takes one digit and outputs a line that looks something like the following:<br>
`mov	w8, #3`<br>

Where the 3 represents the digit I provided to the program.

This asm code was obtained on my Mac M4 (ARM) machine by writing a C program and compiling it so it would produce the ASM for me.<br>

Here are the steps I used to do that:<br>
1. wrote very simple C program (full program below) which sets a int var to a value.
2. compiled using gcc with the following command line: `$ gcc basic.c -o basic -save-temps` Note: -save-temps produces `basic.s` (asm) file.
3. Examined file for the asm command that sets the int value (mov)
4. Copied and pasted the code into the dotnet program for output

Full C listing & associated .s (asm)
#### Simple C Program used for obtaining ASM
```
#include <stdio.h>


int main(){
    int x = 4;
    return x;
}
```
#### ASM Produced From GCC -save-temps (basic.s)
```
	.section	__TEXT,__text,regular,pure_instructions
	.build_version macos, 15, 0	sdk_version 15, 1
	.globl	_main                           ; -- Begin function main
	.p2align	2
_main:                                  ; @main
	.cfi_startproc
; %bb.0:
	sub	sp, sp, #16
	.cfi_def_cfa_offset 16
	str	wzr, [sp, #12]
	mov	w8, #4                          ; =0x4
	str	w8, [sp, #8]
	ldr	w0, [sp, #8]
	add	sp, sp, #16
	ret
	.cfi_endproc
                                        ; -- End function
```

#### You Can Link the .o Yourself
Once you obtain the output from the GCC compiler using the command to generate the .s (also produces the .o (object file), then you can link it yourself if you want with the following command.
<br>
`ld -o basic basic.o`<br>

That will create an exe named basic by using the basic.o and the linker (ld).

### Alter the ASM & Re-compile It

You could take the `basic.s` file above, make a change and then recompile it to its `basic.o` file and then relink it to create the final exe.<br>
For example, let's add 5 (increment) to w8.<br>
1. after the `mov w8, #4` line, add the following line of code: `add  w8, w8, #5`
2. that new line adds 5 to the current value in w8 (4) and then stores the result in w8
3. now recompile the basic.s file with `asm basic.s -o basic.o`
4. link the file again (to create the exe) ld basic.o -o basic
5. run it and get the return value (value last stored in w8) : `$ ./basic`
6. echo the last return value: `$ echo $?`
7. You should see the value 9

That's it!  

