build: hanko.exe
license:
	cat LICENSE


src:=Program.cs \

hanko.exe: $(src)
	mono-csc -out:$@ $(cs_flags) $^

