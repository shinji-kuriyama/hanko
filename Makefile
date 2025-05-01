build: hanko.exe
license:
	cat LICENSE


src:=Program.cs \
     Form1.cs \

cs_flags:=-r:System.Windows.Forms.dll \

hanko.exe: $(src)
	mono-csc -out:$@ $(cs_flags) $^

