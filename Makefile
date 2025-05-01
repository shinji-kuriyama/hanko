build: hanko.exe
license:
	cat LICENSE


bin:=hanko.exe
src:=Program.cs \
     Form1.cs \
     configs.cs \
     hanko.cs \
     options.cs \
     testdata.cs \

cs_flags:=-r:System.Windows.Forms.dll \
          -r:System.Drawing.dll \
          -r:WindowsBase.dll \

run: $(bin)
	./$< $(args)
$(bin): $(src)
	mono-csc -out:$@ $(cs_flags) $^

