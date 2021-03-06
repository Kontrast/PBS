﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Core.Processors
{
    /// <summary>
    /// The resampler class
    /// </summary>
    public class Resampler
    {
        unsafe public Samples Resample(Samples source, float targetBitrate)
        {
            var values = source.Values;
            var k = targetBitrate / source.BitRate;
            var newLength = (int)Math.Round(k * source.Values.Length);
            var resValues = new float[newLength];
            var l = values.Length;

            fixed (float* valuesPtr = values)
            {
                var ptr = valuesPtr;
                for (int i = 0; i < l; i++)
                {
                    var j = (int)(k * i);
                    resValues[j] += *ptr;
                    ptr++;
                }
            }

            l = resValues.Length;

            fixed (float* valuesPtr = resValues)
            {
                var ptr = valuesPtr;
                for (int i = 0; i < l; i++)
                {
                    *ptr *= k;
                    ptr++;
                }
            }

            return new Samples() { Values = resValues, BitRate = targetBitrate };
        }
    }
}
