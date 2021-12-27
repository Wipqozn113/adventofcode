class Cuboid:
    def __init__(self, x1, x2, y1, y2, z1, z2):
        self.x1 = x1
        self.x2 = x2
        self.y1 = y1
        self.y2 = y2
        self.z1 = z1
        self.z2 = z2

    def Explode(self, other):
        # New cubes
        cubes = []

        return cubes

    def Merge(self, cubes):
        pass
    
    def Overlapping(self, other):
        if self.IsNested(other):
            return True
        elif other.IsNested(self):
            return True

        return False

    def IsCompletelyNested(self, other):
        ox1 = other.x1 if other.x2 > other.x1 else other.x2
        ox2 = other.x2 if other.x2 > other.x1 else other.x1      
        oy1 = other.y1 if other.y2 > other.y1 else other.y2
        oy2 = other.y2 if other.y2 > other.y1 else other.y1     
        oz1 = other.z1 if other.z2 > other.z1 else other.z2
        oz2 = other.z2 if other.z2 > other.z1 else other.z1

        return ((self.x1 in range(ox1, ox2+1) and self.x2 in range(ox1, ox2+1)) 
            and (self.y1 in range(oy1, oy2+1) and self.y2 in range(oy1, oy2+1))
            and (self.z1 in range(oz1, oz2+1) and self.z2 in range(oz1, oz2+1)))

    def IsNested(self, other):
        ox1 = other.x1 if other.x2 > other.x1 else other.x2
        ox2 = other.x2 if other.x2 > other.x1 else other.x1
        x1nested = self.x1 in range(ox1, ox2+1)
        if not x1nested:
            x2nested = self.x2 in range(ox1, ox2+1)
            if not x2nested:
                return False

        
        oy1 = other.y1 if other.y2 > other.y1 else other.y2
        oy2 = other.y2 if other.y2 > other.y1 else other.y1        
        y1nested = self.y1 in range(oy1, oy2+1)
        if not y1nested:
            y2nested = self.y2 in range(oy1, oy2+1)
            if not y2nested:
                return False

        oz1 = other.z1 if other.z2 > other.z1 else other.z2
        oz2 = other.z2 if other.z2 > other.z1 else other.z1
        z1nested = self.z1 in range(oz1, oz2+1)
        if not z1nested:
            z2nested = self.z2 in range(oz1, oz2+1)
            if not z2nested:
                return False

        return True

cubes = []
with open('p1.in') as file:
    for line in file:
        comm, ranges = line.strip().split(" ")
        ranges = ranges.split(",")
        n = ranges[0].find("..")
        x1 = int(ranges[0][2:n])
        x2 = int(ranges[0][n+2:])
        n = ranges[1].find("..")
        y1 = int(ranges[1][2:n])
        y2 = int(ranges[1][n+2:])
        n = ranges[2].find("..")
        z1 = int(ranges[2][2:n])
        z2 = int(ranges[2][n+2:])
        cube = Cuboid(x1, x2, y1, y2, z1, z2)
        overlappingcubes = []
        for cub in cubes:
            if cube.Overlapping(cub):
                overlappingcubes.append(cub)
                
                # If the cube is completely nested inside another cube, then it's the only cube we need to worry about
                if cube.IsCompletelyNested(cub):   
                    overlappingcubes = cub
                    break


        # No overlapping cubes found, so there's nothing to turn off
        if len(overlappingcubes) == 0 and comm == "off":    
            continue     
        # No overlapping cubes found, so just create the cube as is   
        elif len(overlappingcubes) == 0 and comm == "on":               
            cubes.append(cube)
        # Overlapping cubes found, so we need merge cubes
        elif len(overlappingcubes) > 0 and comm == "on":
            # Cube is compeltely nested in a single cube, so do nothing
            if(len(overlappingcubes == 1) and cube.IsCompletelyNested(cub)):
                continue
            newcubes = cube.Merge(overlappingcubes)
            for cub in overlappingcubes:
                cubes.remove(cub)
            cubes += newcubes
        # Overlapping cubes found, will need to explode cubes
        elif len(overlappingcubes) > 0 and comm == "off":
            for cub in overlappingcubes:
                newcubes = cub.Explode(cube)
                cubes.remove(cub)
                cubes += newcubes


count = 0
print(count)