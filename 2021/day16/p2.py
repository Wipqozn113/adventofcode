class Packet:
    def __init__(self, hex=None, binary=None, val=None):
        if hex is not None:
            binary = self.DecodeHex(hex)

        self.bits = binary  
        self.version = int(binary[:3], 2)   
        self.type = int(binary[3:6], 2)   
        self.subpackets = []

        if val is None and self.type == 4:
            n = 6
            val = ""
            while True:
                val += self.bits[n+1:n+5]
                n += 5
                if self.bits[n-5] == "0":
                    break

        self.val = None if val is None else int(val, 2)


    def PopulateSubpackets(self):
        type, length, packets = self.Packets()
        while True:
            v = int(packets[:3], 2)
            t = int(packets[3:6], 2)

            if t == 4:
                n = 6
                val = ""
                while True:
                    val += packets[n+1:n+5]
                    n += 5
                    if packets[n-5] == "0":
                        break

                self.subpackets.append(Packet(binary=packets[:n], val=val))   
                packets = packets[n:]
            else:
                l = len(packets)
                packet = Packet(binary=packets)
                packets = packet.PopulateSubpackets()
                self.subpackets.append(packet)
                n = len(packets) - l

            if "1" not in packets:
                break

            if type == 0:     
                length -= n
                if length <= 0:
                    break

            elif type == 1:
                if len(self.subpackets) == length:
                    break
            
        return packets

    def Packets(self):
        if self.bits[6] == "0":
            length = int(self.bits[7:22], 2)
            packets = self.bits[22:]
        elif self.bits[6] == "1":
            length = int(self.bits[7:18], 2)
            packets = self.bits[18:]
        return int(self.bits[6]), length, packets


    @property
    def IsLiteral(self):
        return self.type == 4

    @property 
    def IsOperator(self):
        return self.type != 4

    def DecodeHex(self, hex):
        decoded = ""
        for h in hex:
            decoded += bin(int(h, 16))[2:].zfill(4)
        return decoded

    def VersionSum(self):
        sum = self.version
        for packet in self.subpackets:
            sum += packet.VersionSum()
        return sum

    @property
    def Value(self):
        val = None
        if self.IsLiteral:
            val =  self.val

        elif self.type == 0:
            # sum
            val = 0
            for packet in self.subpackets:                
                val += packet.Value

        elif self.type == 1:
            # product
            val = 1

            for packet in self.subpackets:
                val *= packet.Value

        elif self.type == 2:
            # minimum
            val = None
            for packet in self.subpackets:
                if val is None or val > packet.Value:
                    val = packet.Value

        elif self.type == 3:
            # maximum
            val = None
            for packet in self.subpackets:
                if val is None or val < packet.Value:
                    val = packet.Value
        
        elif self.type == 5:
            # greater than
            val = int(self.subpackets[0].Value > self.subpackets[1].Value)

        elif self.type == 6:
            # less than
            val = int(self.subpackets[0].Value < self.subpackets[1].Value)

        elif self.type == 7:
            # equal to
            val = int(self.subpackets[0].Value == self.subpackets[1].Value)

        return val

with open('s2.in') as file:
    for line in file:
        l = line.split("|")
        # Test case
        packet = Packet(hex=l[0])
        packet.PopulateSubpackets()
        if(len(l) == 2):
            print(packet.Value, l[1])
        else:
            print(packet.Value)
