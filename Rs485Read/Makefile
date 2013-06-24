CC = mcs

SRCS = $(wildcard *.cs)

all: rs485_read.exe
rs485_read.exe:  $(SRCS)
	$(CC) $^ -out:$@
	chmod +x $@
clean:
	rm -f rs485_read.exe