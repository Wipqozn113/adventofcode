class Seed:
    def __init__(self, seed_num):
        self.seed_num = seed_num
        self.soil_num = None
        self.fert_num = None
        self.water_num = None
        self.light_num = None
        self.temp_num = None
        self.humid_num = None
        self.loc_num = None

    def debug(self):
        print(self.seed_num, self.soil_num, 
        self.fert_num, self.water_num, 
        self.light_num, self.temp_num,
        self.humid_num, self.loc_num)

    def perform_mappings(self, mappers):
        self.soil_num = mappers.map(self.seed_num, "seed")
        self.fert_num = mappers.map(self.soil_num, "soil")
        self.water_num = mappers.map(self.fert_num, "fertilizer")
        self.light_num = mappers.map(self.water_num, "water")
        self.temp_num = mappers.map(self.light_num, "light")
        self.humid_num = mappers.map(self.temp_num, "temperature")
        self.loc_num = mappers.map(self.humid_num, "humidity")

class Mappers:
    def __init__(self):
        self.mappers = {}
    
    def add_mapper(self, mapper):
        self.mappers[mapper.src_name] = mapper

    def map(self, src_num, src_type):
        return self.mappers[src_type].map(src_num)        

class Mapper:
    def __init__(self, source, destination):
        self.src_name = source
        self.dest_name = destination
        self.ranges = []
    
    def add_range(self, line):
        ln = line.split()
        self.ranges.append([int(ln[1]), int(ln[0]), int(ln[2])])

    def map(self, src):
        for ran in self.ranges:
            if ran[0] <= src <= ran[0] + ran[2]:                
                return ran[1] + (src - ran[0])
        return src

def parse_seeds(line):
    seeds = line.split(":")[1].strip().split()
    sds = []
    while len(seeds) > 0:
        ran = int(seeds.pop())
        start = int(seeds.pop())
        for seed in range(start, start + ran):
            sds.append(Seed(seed))
    return sds


seeds = []
mappers = Mappers()
filename = "test.in"

with open(filename) as file:
    mapper = None
    for line in file:
        if line[:6] == "seeds:":
            seeds = parse_seeds(line)       
        elif line[0].isdigit():
            mapper.add_range(line)
        elif line.strip()[-4:] == "map:":
            ln = line.split()[0].split("-to-")
            mapper = Mapper(ln[0], ln[1])
            mappers.add_mapper(mapper)


for seed in seeds:
    seed.perform_mappings(mappers)

lowest = seeds[0].loc_num
for seed in seeds:
    lowest = seed.loc_num if seed.loc_num < lowest else lowest
print(lowest)


