class Coordinates:
    def __init__(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z

class Rotation:
    def __init__(self, x, y, z):
        self.rotations = [
            # x is facing x
            [x, y, z],
            [x, -z, y],
            [x, -y, -z],
            [x, z, -y],
            # x is facing -x
            [-x, -y, z],
            [-x, -z, -y],
            [-x, y, -z],
            [-x, z, y],
            # x is facing y
            [-z, x, -y],
            [y, x, -z],
            [z, x, y],
            [-y, x, z],
            # x is facing -y
            [z, -x, -y],
            [y, -x, z],
            [-z, -x, y],
            [-y, -x, -z],
            # x is facing z
            [-y, -z, x],
            [z, -y, x],
            [y, z, x],
            [-z, y, x],
            # x is facing -z
            [z, y, -x],
            [-y, z, -x],
            [-z, -y, -x],
            [y, -z, -x]
        ]
    
    def Print(self):
        facings = ["x", "-x", "y", "-y", "z", "-z"]
        i = 0
        n = 0
        for rotation in self.rotations:
            if i % 4 == 0:
                print(f"X is facing {facings[n]}")
                n += 1
                
            i += 1
            print(rotation)

class Beacon:
    def __init__(self, coordinates):
        self.coord = coordinates

class Scanner:
    def __init__(self, id, beacons=[], coordinates=None):
        self.id = id
        self.beacons = []
        self.coord = coordinates
        self.rotation = Rotation(1, 1, 1)
    
    def SetCoordinates(self, x, y, z):
        self.coord = Coordinates(x, y, z)

    def AddBeacon(self, beacon):
        self.beacons.append(beacon)

    def Overlapping(self, scanner):
        pass

scanners = []
rotations = []
with open('s2.in') as file:
    for line in file:
        if line[0] == "-":
            scanner = Scanner(len(scanners))
            if len(scanners) == 1:
                scanner.SetCoordinates(0, 0, 0)
        elif line.strip() == "":
            continue
        else:
            l = line.strip().split(",")
            beacon = Beacon(Coordinates(l[0], l[1], l[2]))
            rotations.append(Rotation(int(l[0]), int(l[1]), int(l[2])))
            scanner.AddBeacon(beacon)

print(rotations[0].Print())
# mainscanner = scanners[0]
