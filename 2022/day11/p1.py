import math

class Item:
    def __init__(self, worry_level):
        self.worry_level = int(worry_level)

    def UpdateWorry(self, worry):
        self.worry_level = math.floor(worry // 3)

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
        self.fxstr = fx
        self.fx = lambda old: eval(fx)
    
    def Take(self, item):
        self.items.append(item)

    def Turn(self, monkeys):
        # Throw monkeys haven't been set yet
        if isinstance(self.truemon, int):
            self.truemon = monkeys[self.truemon]
            self.falsemon = monkeys[self.falsemon]           


        for item in self.items:
            self.Inspect(item)
            self.Throw(item)
        self.items = []

    def Throw(self, item):
        if item.worry_level % self.divisor == 0:
            self.truemon.Take(item)
        else:
            self.falsemon.Take(item)
    
    def Inspect(self, item):
        self.inspects += 1
        before = item.worry_level
        fx = self.fx(item.worry_level)
        item.UpdateWorry(self.fx(item.worry_level))
        after = item.worry_level

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
                print(fx)
                monkeys[i].SetFx(fx)
            elif line.startswith("  Test:"):
                divisor = line.split(" ")[-1]
                print(divisor)
                monkeys[i].divisor = int(divisor)
            elif line.startswith("    If true:"):
                mon = line.split(" ")[-1]
                monkeys[i].truemon = int(mon)
            elif line.startswith("    If false:"):
                mon = line.split(" ")[-1]
                monkeys[i].falsemon= int(mon)
    return monkeys

def RunRounds(monkeys, rounds):
    for round in range(rounds):
        for monkey in monkeys:
            monkey.Turn(monkeys)


def CalcMonkeyBusiness(monkeys):
    monkeys.sort(key=lambda x: x.inspects, reverse=True)
    print(monkeys[0].inspects, monkeys[1].inspects)
    return monkeys[0].inspects * monkeys[1].inspects

def PrintMonkeys(monkeys):
    for monkey in monkeys:
        monkey.PrintMe()

monkeys = CreateMonkeys("input.in")
#PrintMonkeys(monkeys)
RunRounds(monkeys, 20)
print(CalcMonkeyBusiness(monkeys))


