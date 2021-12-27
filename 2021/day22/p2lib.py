from aabbtree import AABB
from aabbtree import AABBTree

oncubes = None
offcubes = None
with open('s3.in') as file:
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
        if comm == "on":
            if oncubes is None:
                oncubes = AABB([(x1, x2), (y1, y2), (z1, z2)])
            else:
                temp = AABB([(x1, x2), (y1, y2), (z1, z2)])
                oncubes = AABB.merge(oncubes, temp)
        elif comm == "off":
            if offcubes is None:
                offcubes = AABB([(x1, x2), (y1, y2), (z1, z2)])
            else:
                temp = AABB([(x1, x2), (y1, y2), (z1, z2)])
                offcubes = AABB.merge(offcubes, temp)

print(oncubes.volume - oncubes.overlap_volume(offcubes))


