#ifndef __UNMANAGEDONSCLIENTEXCEPTION_H__
#define __UNMANAGEDONSCLIENTEXCEPTION_H__

#include <exception>
#include "ONSClient.h"

namespace ons{

class ONSCLIENT_API UnmanagedONSClientException: public std::exception
{
    public:
        UnmanagedONSClientException() throw();
        virtual ~UnmanagedONSClientException() throw();
        UnmanagedONSClientException(std::string msg, int error) throw();
        const char* what() const throw();
        int GetError() const throw();
        
    private:
        std::string  m_msg;
        int             m_error;
};

}


#endif
