class House:
    def __init__(self, x, y):
        self.x = x
        self.y = y
        self.count = 1

filename = "input.in"
houses = []
house = House(0,0)
curr = [0, 0]
houses.append(house)
with open(filename) as file:
    for line in file:
        for ln in line.strip():
            if ln == ">":
                curr[0] += 1
            elif ln == "<":
                curr[0] -= 1
            elif ln == "^":
                curr[1] += 1
            elif ln == "v":
                curr[1] -= 1
            found = False
            for hs in houses:                
                if hs.x == curr[0] and hs.y == curr[1]:
                    hs.count += 1
                    found = True
                    break
            if not found:
                house = House(curr[0], curr[1])
                houses.append(house)
                
print(len(houses))                
    
