import re

class Coord:
    def __init__(self, x, y):
        self.x = x
        self.y = y
    
class PartNumber:
    def __init__(self, val, x, y):
        self.val = val
        self.start = Coord(x, y)
        self.end = None

    def SetEnd(self, x, y):
        self.end = Coord(x, y)

    def IsPartnumber(self, symbols):
        for symbol in symbols:
            if abs(symbol.coord.y - self.start.y) <= 1 and (self.start.x - 1 <= symbol.coord.x <= self.end.x + 1):
                return True
        return False

class Symbol:
    def __init__(self, symbol, x, y):
        self.symbol = symbol
        self.coord = Coord(x, y)

filename = "input.in"
partnumbers = []
symbols = []

with open(filename) as file:
    y = 0
    for line in file:        
        numbers = re.findall('[0-9]+', line)
        numbers.reverse()
        x = 0
        checking = False

        for ln in line:
            if ln.isdigit() and not checking:
                partnum = PartNumber(int(numbers.pop()), x, y)
                partnumbers.append(partnum)
                checking = True
            elif not ln.isdigit() and checking:
                partnum.SetEnd(x - 1, y)
                checking = False

            if not ln.isdigit() and ln != "." and ln != "\n":
                sym = Symbol(ln, x, y)    
                symbols.append(sym)            

            x += 1
        y += 1        

total = 0
for pn in partnumbers:
    total += pn.val if pn.IsPartnumber(symbols) else 0
print(total)


