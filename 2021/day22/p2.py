size = 101
cubes = [[[0 for k in range(size)] for j in range(size)] for i in range(size)]

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
        print(line)        
        for x in range(x1, x2+1):
            if x > 50 or x < -50:
                continue
            for y in range(y1, y2+1):
                if y > 50 or y < -50:
                    continue
                for z in range(z1, z2+1):
                    if z > 50 or z < -50:
                        continue
           
                    cubes[x][y][z] = 1 if comm == "on" else 0

count = 0
for x in range(-50, 51):
    for y in range(-50, 51):
        for z in range(-50, 51):
            count += cubes[x][y][z]
        
print(count)