build: hanko.exe
license:
	cat LICENSE


bin:=hanko.exe
src:=Program.cs \
     Form1.cs \
     configs.cs \
     options.cs \

cs_flags:=-r:System.Windows.Forms.dll \

run: $(bin)
	./$<
$(bin): $(src)
	mono-csc -out:$@ $(cs_flags) $^

