#ifndef __CONSUMECONTEXT_H__
#define __CONSUMECONTEXT_H__

#include "UnManagedConsumeContext.h"

namespace ons{

public ref class  ConsumeContext
{
    public:
        ConsumeContext();
        ConsumeContext(ConsumeContext% other);
        virtual ~ConsumeContext();
        
        UnmanagedConsumeContext* getConsumeContext();
    private:
        UnmanagedConsumeContext* m_consumerContext;
};

}

#endif
