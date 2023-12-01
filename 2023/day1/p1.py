def FindCode(input):
    i = 0
    first = None
    last = None
    while True:
        if first is None:
            if input[i].isdigit():
                first = int(input[i])

        if last is None:
            if input[-(i+1)].isdigit():
                last = int(input[-(i+1)])

        i += 1

        if first is not None and last is not None:
            return (first * 10) + last
        
def FindTotal(filename="input.in"):
    total = 0
    with open(filename) as file:
        for line in file:
            total += FindCode(line)

    return total

print(FindTotal())