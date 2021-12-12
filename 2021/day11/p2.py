class Octopus:
    def __init__(self, energy):
        self.energy = int(energy)
        self.lastflashed = None
        self.flashes = 0

class Cavern:
    def __init__(self, cavern):
        self.cavern = cavern
        self.flashes = 0
        self.flashesnow = 0

    def StepXTimes(self, step):
        n = 0
        while n < step:
            for x in range(10):
                for y in range(10):
                    self.Charge(n, x, y)

            n += 1

    def FindAllFlash(self):
        n = 0
        while True:
            for x in range(10):
                for y in range(10):
                    self.Charge(n, x, y)
  
            if self.flashesnow == 100:
                return n + 1

            n += 1 
            self.flashesnow = 0


    def Charge(self, step, x, y):
        if (x < 0 or y < 0 or x >=  10 or y >= 10) or self.cavern[x][y].lastflashed == step:
            return

        self.cavern[x][y].energy += 1
        if self.cavern[x][y].energy > 9:
            self.cavern[x][y].lastflashed = step
            self.cavern[x][y].energy = 0
            self.cavern[x][y].flashes += 1
            self.flashes += 1
            self.flashesnow += 1

            self.Charge(step, x + 1, y - 1)
            self.Charge(step, x + 1, y)
            self.Charge(step, x + 1, y + 1)
            self.Charge(step, x, y + 1)
            self.Charge(step, x, y - 1)
            self.Charge(step, x - 1, y)
            self.Charge(step, x - 1, y + 1)
            self.Charge(step, x - 1, y - 1)
        
        return

inp = []
with open('p1.in') as file:
    for line in file:
        inp.append([Octopus(x) for x in list(line.strip())])

cavern = Cavern(inp)
print(cavern.FindAllFlash())
