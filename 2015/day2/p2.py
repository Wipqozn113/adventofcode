class Present:
    def __init__(self, length, width, height):
        self.l = length
        self.w = width
        self.h = height

    def calc_paper(self):
        s1 = self.l * self.w
        s2 = self.w * self.h
        s3 = self.h * self.l
        
        return 2 * (s1 + s2 + s3) + min(s1, s2, s3)
    
    def calc_ribbon(self):
        sides = [self.l, self.w, self.h]
        sides.sort()
        
        return (2 * (sides[0] + sides[1])) + (self.l * self.w * self.h)
    
filename = "input.in"
total = 0
with open(filename) as file:
    for line in file:
        l, w, h = line.strip().split("x")
        present = Present(int(l), int(w), int(h))
        total += present.calc_ribbon()

print(total)