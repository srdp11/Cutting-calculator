using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cutting_calculator
{
    class PartInfo
    {
        private struct CuttingStep
        {
            public Int32 cuttingNum;
            public String partForCut;
            public Int32 residueAfterCut;
            public String residuePartName;

            public CuttingStep(Int32 cuttingNum, String partForCut, Int32 residueAfterCut, String residuePartName)
            {
                this.cuttingNum = cuttingNum;
                this.partForCut = partForCut;
                this.residueAfterCut = residueAfterCut;
                this.residuePartName = residuePartName;
            }
        }

        private String partName;
        private Int32 orderNum;
        private Int32 requiredForCutting;
        private Boolean isCutted;
        private List<CuttingStep> steps;

        public PartInfo(String partName, Int32 orderNum, Int32 requiredForCutting)
        {
            this.partName = partName;
            this.orderNum = orderNum;
            this.isCutted = false;
            this.requiredForCutting = requiredForCutting;
            this.steps = new List<CuttingStep>();
        }

        public String PartName
        {
            get
            {
                return partName;
            }
        }

        public Boolean IsCutted
        {
            get
            {
                return isCutted;
            }

            set
            {
                isCutted = value;
            }
        }

        public Int32 OrderNum
        {
            get
            {
                return orderNum;
            }
        }

        public Int32 RequiredForCutting
        {
            get
            {
                return requiredForCutting;
            }
        }

        public void AddCutStep(Int32 cuttingNum, String partForCut, Int32 residueAfterCut, String residuePartName)
        {
            CuttingStep step = new CuttingStep(cuttingNum, partForCut, residueAfterCut, residuePartName);
            steps.Add(step);
        }

    }
}
