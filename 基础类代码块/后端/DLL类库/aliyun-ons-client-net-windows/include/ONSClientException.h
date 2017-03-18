#ifndef __ONSCLIENTEXCEPTION_H__
#define __ONSCLIENTEXCEPTION_H__

#include "UnManagedONSClientException.h"

namespace ons{

public ref class ONSClientException: public System::Exception{
    public:
        ONSClientException() ;
        virtual ~ONSClientException() ;
        ONSClientException(char* msg, int error) ;
        System::String^ what();
        int GetError();
        

    private:
        System::String^  m_msg;
        int     m_error;
};

}
#endif