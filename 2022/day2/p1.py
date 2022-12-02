class Rock:
    def __init__(self):
        self.points = 1
        
    def Fight(self, other):
        if(type(other) is Scissors):
            pt = 6
        elif(type(other) is Rock):
            pt =  3
        else:
            pt = 0

        return (pt + self.points)

class Paper:
    def __init__(self):
        self.points = 2

    def Fight(self, other):
        if(type(other) is Rock):
            pt = 6
        elif(type(other) is Paper):
            pt = 3
        else:
            pt = 0

        return (pt + self.points)

class Scissors:
    def __init__(self):
        self.points = 3
        
    def Fight(self, other):
        if(type(other) is Paper):
            pt = 6
        elif(type(other) is Scissors):
            pt =  3
        else:
            pt = 0

        return (pt + self.points)

def Fight(elf, me):
    if(elf == "A"):
        e = Rock()
    elif(elf == "B"):
        e = Paper()
    else:
        e = Scissors()

    if(me == "X"):
        m = Rock()
    elif(me == "Y"):
        m = Paper()
    else:
        m = Scissors()

    return m.Fight(e)

score = 0
with open('input.in') as file:
    for line in file:
        l = line.split(" ")
        elf = l[0].strip()
        me = l[1].strip()

        score += Fight(elf, me)

print(score)