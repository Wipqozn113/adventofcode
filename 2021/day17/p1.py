class TargetArea:
    def __init__(self, x1, x2, y1, y2):
        self.x1 = x1 if x1 < x2 else x2
        self.x2 = x2 if x2 > x1 else x1
        self.y1 = y1 if y1 < y2 else y2
        self.y2 = y2 if y2 > y1 else y1

    def InTargetArea(self, x, y):
        return (self.x1 <= x <= self.x2) and (self.y1 <= y <= self.y2)
    
    def PassedTargetArea(self, x, y):
        return (x > self.x2) or (y < self.y1)


class Probe:
    def __init__(self, x, y, targetarea):
        self.initial_x_vel = x
        self.initial_y_vel = y
        self.targetarea = targetarea
        self.maxheight = None

    def HitsTargetArea(self):
        x = 0
        y = 0
        xv = self.initial_x_vel
        yv = self.initial_y_vel
        self.maxheight = 0
        while True:
            x += xv 
            y += yv
            self.maxheight = y if y > self.maxheight else self.maxheight
            xv += 0 if xv == 0 else -1 if xv > 0 else 1
            yv -= 1
            if self.targetarea.InTargetArea(x, y):
                return True
            elif self.targetarea.PassedTargetArea(x, y):
                return False

# target area: x=20..30, y=-10..-5
with open('p1.in') as file:
    for line in file:
        line = line.split(":")
        line = line[1].split(",")
        lx = line[0].split("..")
        ly = line[1].split("..")
        targetarea = TargetArea(int(lx[0][3:]), int(lx[1]), int(ly[0][3:]), int(ly[1]))

maxheight = 0
for x in range(targetarea.x2):
    for y in range(targetarea.x2):
        probe = Probe(x, y, targetarea)
        if probe.HitsTargetArea():
            maxheight = probe.maxheight if probe.maxheight > maxheight else maxheight

print(maxheight)