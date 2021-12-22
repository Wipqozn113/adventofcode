class Coordinates:
    def __init__(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z
        self.coord = [x, y, z]

    def ManhattenDistance(self, other):
        if self == other:
            return 0

        return abs(self.x - other.x) + abs(self.y - other.y) + abs(self.z - other.z)
    
    def __add__(self, other):
        return Coordinates(self.x + other.x, self.y + other.y, self.z + other.z)

    def __sub__(self, other):
        return Coordinates(self.x - other.x, self.y - other.y, self.z - other.z)

    def __eq__(self, other):
        return (self.x == other.x) and (self.y == other.y) and (self.z == other.z)

coordinates = []
with open('p2.in') as file:
    for line in file:
        l = line.strip().split(" ")
        if(len(l) == 3):
            coord = Coordinates(int(l[0]), int(l[1]), int(l[2]))
            coordinates.append(coord)

largest = 0
for coord1 in coordinates:
    for coord2 in coordinates:
        dist = coord1.ManhattenDistance(coord2)
        largest = dist if dist > largest else largest

print(largest)
