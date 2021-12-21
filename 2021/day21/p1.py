class Player:
    def __init__(self, position, die):
        self.pos = position
        self.score = 0
        self.die = die
    
    def Move(self, rolls=3):
        val = self.die.Roll(rolls)
        pos = (self.pos + val) % 10
        self.pos = 10 if pos == 0 else pos
        self.score += self.pos

        return self.score >= 1000

class Die:
    def __init__(self):
        self.rollcount = 0
        self.lastroll = 0

    def Roll(self, rolls):
        self.rollcount += rolls
        val = 0

        for n in range(rolls):
            self.lastroll += 1
            if self.lastroll == 101:
                self.lastroll = 1
            val += self.lastroll    

        return val

p1 = None
p2 = None
die = Die()
with open('p1.in') as file:
    for line in file:
        line = line.split(":")
        if p1 == None:
            p1 = Player(int(line[1].strip()), die)
        else:
            p2 = Player(int(line[1].strip()), die)

while True:
    if p1.Move():
        print(p2.score * die.rollcount)
        exit()

    if p2.Move():
        print(p1.score * die.rollcount)
        exit()



