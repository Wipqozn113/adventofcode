import math

class AddX:
    def __init__(self, val, cycle):
        self.val = val
        self.cycle = cycle + 2


class CPU:
    def __init__(self):
        self.cycle = 0
        self.x = 1
        self.signal_strengths = {}
        self.next_add = None
        self.screen = [['.']*40 for i in range(6)]

    def PrintScreen(self):
        for row in self.screen:
            line = ""
            for pixel in row:
                line += pixel
            print(line)
    
    def Tick(self, filename):
        signalsums = 0
        check_at = 20
        with open(filename) as file:
            for input in file:
                if input.startswith('addx'):
                    inp = int(input.split(' ')[1])
                    self.next_add = AddX(inp, self.cycle)
                while True:
                    self.cycle += 1
                    self.signal_strengths[self.cycle] = self.cycle * self.x

                    pos = (self.cycle - 1) % 40
                    if(abs(self.x - pos) < 2):
                        v = math.floor((self.cycle/40))
                        self.screen[v][pos] = '#'
                     
                    if self.cycle == check_at:
                      #  print(self.cycle, self.x, self.signal_strengths[self.cycle])  
                        check_at += 40
                        signalsums += self.signal_strengths[self.cycle]
                    if self.next_add is not None and self.next_add.cycle == self.cycle:     
                       # print(self.cycle, self.next_add.val)               
                        self.x += self.next_add.val
                        self.next_add = None
                    elif self.next_add is not None:
                        continue
                    
                    break

        print("Sums:", signalsums)
        self.PrintScreen()

        
cpu = CPU()
cpu.Tick("input.in")



