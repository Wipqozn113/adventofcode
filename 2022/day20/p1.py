from math import floor, ceil

class EncNum:
    def __init__(self, num, pos):
        self.num = num
        self.pos = pos

class EncFile:
    def __init__(self, file):
        self.org = []
        self.mixer = []
        self.zero = None
        self.len = len(file)
        n = 0
        for num in file:
            enc = EncNum(num, n)
            if num == 0:
                self.zero = enc
            self.org.append(enc)
            self.mixer.append(enc)
            n += 1


    def MixFile(self):
        
        for org in self.org:
            idx = self.mixer.index(org)
            self.mixer.remove(org)

            move = 0
            if org.num > 0:
                move = org.num % (self.len - 1)
            elif org.num < 0:
                move = (org.num * -1) % (self.len - 1)
                move *= -1

            pos = idx + move
            if pos > self.len:
                pos = (pos + 1) % self.len
            elif pos < 0:
                pos = (pos - 1) % self.len
            self.mixer.insert(pos, org)
            
    def PrintMixer(self):
        mx = ""
        for mix in self.mixer:
            mx += str(mix.num) + ","      

    def GroveNumbersSum(self):
        idx = self.mixer.index(self.zero)
        grove = 0
        self.PrintMixer()
        for i in range(1, 4):
            grove += self.mixer[(idx + (1000 * i)) % self.len].num

        return grove


def CreateFile(filename):
    input = []
    with open(filename) as file:
        for line in file:
            input.append(int(line.strip()))

    return input

file = CreateFile("input.in")
encfile = EncFile(file)
encfile.MixFile()
print(encfile.GroveNumbersSum())