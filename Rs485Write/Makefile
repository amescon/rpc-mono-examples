CC = mcs

SRCS = $(wildcard *.cs)

all: rs485_write.exe
rs485_write.exe:  $(SRCS)
	$(CC) $^ -out:$@
	chmod +x $@
clean:
	rm -f rs485_write.exe