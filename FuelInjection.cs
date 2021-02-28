//Problem Statement
//=================
//Commander Lambda has asked for your help to refine the automatic quantum antimatter fuel injection system for her LAMBCHOP doomsday device. 
//It's a great chance for you to get a closer look at the LAMBCHOP 
//and maybe sneak in a bit of sabotage while you're at it - so you took the job gladly. 

//Quantum antimatter fuel comes in small pellets, which is convenient since the many moving parts of the LAMBCHOP 
//each need to be fed fuel one pellet at a time. However, minions dump pellets in bulk into the fuel intake. 
//You need to figure out the most efficient way to sort and shift the pellets down to a single pellet at a time. 

//The fuel control mechanisms have three operations: 
//1) Add one fuel pellet
//2) Remove one fuel pellet
//3) Divide the entire group of fuel pellets by 2 (due to the destructive energy released when a quantum antimatter pellet is cut in half, 
//    the safety controls will only allow this to happen if there is an even number of pellets)

//Write a function called solution(n) which takes a positive integer as a string and returns the minimum number of operations 
//needed to transform the number of pellets to 1. The fuel intake control panel can only display a number up to 309 digits long, 
//    so there won't ever be more pellets than you can express in that many digits.

//For example:
//solution(4) returns 2: 4 -> 2 -> 1
//solution(15) returns 5: 15 -> 16 -> 8 -> 4 -> 2 -> 1

namespace FooBar
{
    class FuelInjection
    {
        public static int CalculateCycles(string x)
        {
            LargeNumber number = new LargeNumber(x);
            int counter = 0;

            while (true)
            {
                if (number.Value == "1") return counter;

                if (number.isEven()) number.Half();

                else if (number.Value == "3") number.Decrement();

                else if (number.isSecondLastEven() && number.endsWith3or7())
                    number.Increment();

                else if (!number.isSecondLastEven() && !number.endsWith3or7())
                    number.Increment();

                else number.Decrement();

                counter++;
            }
        }
    }

    class LargeNumber
    {
        public string Value { get; private set; }

        private int _length;
        private int _last;

        public LargeNumber(string number)
        {
            Update(number);
        }

        private void Update(string number)
        {
            Value = number;
            _length = number.Length;
            _last = Value[_length - 1] - '0';
        }

        public bool isEven()
        {
            return _last % 2 == 0;
        }
        public bool isSecondLastEven()
        {
            int secondLast = _length < 2 ? 0 : Value[_length - 2] - '0';
            return secondLast % 2 == 0;
        }

        public bool endsWith3or7()
        {
            return _last == 3 || _last == 7;
        }

        public void Increment()
        {
            string str = "";
            int carry = 1;

            for(int i = this._length - 1; i>=0; i--)
            {
                int sub = (int)(this.Value[i] - '0') + carry;

                if (sub >= 10)
                {
                    sub -= 10;
                    carry = 1;
                }
                else carry = 0;

                str = (char)(sub + '0') + str;
            }

            if (carry > 0) str = "1" + str;

            this.Update(str);
        }

        public void Decrement()
        {
            string str = "";
            int carry = 1;

            for (int i = this._length - 1; i >= 0; i--)
            {
                int sub = (int)(this.Value[i] - '0') - carry;

                if (sub < 0)
                {
                    sub += 10; 
                    carry = 1;
                }
                else carry = 0;

                if (i == 0 && sub == 0) continue;
                
                str = (char)(sub + '0') + str;
            }

            this.Update(str);
        }     

        public void Half()
        {
            string ans = "";

            int idx = 0;
            int temp = (int)(this.Value[idx] - '0');
            while (temp < 2)
            {
                temp = temp * 10 + (int)(this.Value[idx + 1] - '0');
                idx++;
            }
            ++idx;

            while (this._length > idx)
            {
                ans += (char)(temp / 2 + '0');
                temp = (temp % 2) * 10 + (int)(this.Value[idx] - '0');
                idx++;
            }
            ans += (char)(temp / 2 + '0');

            if (ans.Length == 0) this.Update("0");

            this.Update(ans);
        }
    }
}
