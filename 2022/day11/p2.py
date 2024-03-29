import time
inspecttime = 0
throwtime = 0

class Item:
    def __init__(self, worry_level):
        self.worry_level = int(worry_level)

    def UpdateWorry(self, worry):
        self.worry_level = worry

class Monkey:
    def __init__(self, name):
        self.name = name
        self.items = []
        self.fx = ''
        self.fxstr = ''
        self.divisor = None
        self.truemon = None
        self.falsemon = None
        self.inspects = 0  
        self.lcm = 0

    def PrintMe(self):
        pr = '''
        Monkey {}:
            Starting items: {}
            Operation: new = {}
            Test: divisible by {}
                If true: throw to monkey {}
                If false: throw to monkey {}
        '''.format(self.name, [x.worry_level for x in self.items], self.fxstr, self.divisor, self.truemon, self.falsemon)
        print(pr)

    def SetFx(self, fx):
        self.fx = lambda old: eval(fx)
    
    def Take(self, item):
        self.items.append(item)

    def SetTrueFalseMons(self, monkeys):
        self.truemon = monkeys[self.truemon]
        self.falsemon = monkeys[self.falsemon]  

    def Turn(self, monkeys):   
        for item in self.items:
            self.Inspect(item)
            self.Throw(item)
        self.items = []

    def Throw(self, item):
        global throwtime
        t = time.time()
        if item.worry_level % self.divisor == 0:
            self.truemon.Take(item)
        else:
            self.falsemon.Take(item)
        throwtime += time.time() - t
    
    def Inspect(self, item):
        global inspecttime
        t = time.time()
        self.inspects += 1
        item.worry_level = self.fx(item.worry_level) % self.lcm
        inspecttime += time.time() - t

def CreateMonkeys(filename):
    i = -1
    monkeys = []
    with open(filename) as file:
        for line in file:
            if line.startswith("Monkey"):
                i += 1
                monkeys.append(Monkey(i))
            elif line.startswith("  Starting items"):
                items = line.split(":")[1].split(",")
                for item in items:
                    it = item.strip()
                    monkeys[i].Take(Item(it))
            elif line.startswith("  Operation:"):
                fx = line.split(":")[1].split("=")[1].strip()
                monkeys[i].SetFx(fx)
            elif line.startswith("  Test:"):
                divisor = line.split(" ")[-1]
                monkeys[i].divisor = int(divisor)
            elif line.startswith("    If true:"):
                mon = line.split(" ")[-1]
                monkeys[i].truemon = int(mon)
            elif line.startswith("    If false:"):
                mon = line.split(" ")[-1]
                monkeys[i].falsemon= int(mon)
    for monkey in monkeys:
        monkey.SetTrueFalseMons(monkeys)
    return monkeys

def RunRounds(monkeys, rounds):
    for round in range(rounds):
        #if(round % 1000 == 0):
         #   print("Round ", round)
        for monkey in monkeys:
            monkey.Turn(monkeys)


def CalcMonkeyBusiness(monkeys):
    monkeys.sort(key=lambda x: x.inspects, reverse=True)    
    return monkeys[0].inspects * monkeys[1].inspects

def PrintMonkeys(monkeys):
    for monkey in monkeys:
        monkey.PrintMe()

def SetLcm(monkeys):
    lcm = 1
    for monkey in monkeys:
        lcm *= monkey.divisor
    for monkey in monkeys:
        monkey.lcm = lcm

    return lcm

start_time = time.time()
monkeys = CreateMonkeys("input.in")
lcm = SetLcm(monkeys)
#PrintMonkeys(monkeys)
RunRounds(monkeys, 10000)
print(CalcMonkeyBusiness(monkeys))
print("--- %s seconds (throw)---" % throwtime)
print("--- %s seconds (inspect) ---" % inspecttime)
print("--- %s seconds ---" % (time.time() - start_time))


