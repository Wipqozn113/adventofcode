class Stack:
    def __init__(self):
        self.stack = []

    def Add(self, crate):
        self.stack.append(crate)

    def Rem(self):
        return self.stack.pop()

    def TopLetter(self):
        return self.stack[len(self.stack) - 1]

class Ship:
    def __init__(self, size):
        self.stacks = []
        for n in range(size):
            self.stacks.append(Stack())

    def StackAdd(self, crate, stack):
        self.stacks[stack].Add(crate)
    
    def Move(self, mv, fr, to):
        stack = self.stacks[to - 1]
        for i in range(mv):        
            stack.Add(self.stacks[fr - 1].Rem())

    def Code(self):        
        code = ""
        for stack in self.stacks:
            code += stack.TopLetter()
        return code

def GetNum(command):
    num = ''
    while True:
        l = command.pop()
        if(l.isdigit()):
            num += l
        elif(len(num) > 0):
            return int(num)

        if(len(command) == 0):
            return int(num)

def CreateShip(lines,size):
    ship = Ship(size)
    lines.reverse()
    for line in lines:
        n = 0
        while n < size:
            crate = line[:4]
            del line[:4]
            if(crate[0] == '['):
                ship.StackAdd(crate[1], n)
            n += 1
    return ship
            

def GetCommands(command):
    command.reverse()
    mv = GetNum(command)
    fr = GetNum(command)
    to = GetNum(command)
    return (mv,fr,to)

ready = False
with open('input.in') as file:
    lines = []
    for line in file:
        if not ready:
            lines.append(list(line))
        if(line.startswith(" 1")):
            size = int(line.strip().split(" ")[-1])
            ship = CreateShip(lines, size)
            ready = True
        if ready and line.startswith("move"):
            mv,fr,to = GetCommands(list(line))
            ship.Move(mv, fr, to)

print(ship.Code())

