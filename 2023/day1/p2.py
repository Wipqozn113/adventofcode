def ConvertNumber(input, i):
    numbers = [('one', 1), ('two', 2), ('three', 3), ('four', 4), ('five', 5), ('six', 6), ('seven', 7), ('eight', 8), ('nine', 9)]
    for number in numbers:
        snum = number[0]
        if snum == input[i : i + len(snum)]:
            return number[1]
    return None

def FindNumber(input, i):
    if input[i].isdigit():
        return int(input[i])
    
    num = ConvertNumber(input, i)
    return num 

def FindCode(input):
    i = 0
    first = None
    last = None
    while True:
        if first is None:
            first = FindNumber(input, i)

        if last is None:
            last = FindNumber(input, -(i+1))

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