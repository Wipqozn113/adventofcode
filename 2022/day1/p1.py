class Elf:
    def __init__(self, cals):
        self.cals = cals

    def TotalCals(self):
        return sum(self.cals)

class Elves:
    def __init__(self):
        self.elves = []

    def AddElf(self, elf):
        self.elves.append(elf)
    
    def Top3(self):
       self.elves.sort(key=lambda x: x.TotalCals())
       top3 = self.elves[-1].TotalCals() + self.elves[-2].TotalCals() + self.elves[-3].TotalCals()
       return top3

food = []
elves = Elves()
maxElf = Elf([])
with open('input.in') as file:
    for line in file:
        if(line.strip() == ''):
            elf = Elf(food)
            elves.AddElf(elf)
            food = []
            if(elf.TotalCals() > maxElf.TotalCals()):
                maxElf = elf
        else:
            food.append(int(line.strip()))

print("P1: " + str(maxElf.TotalCals()))
print("P2: " + str(elves.Top3()))
