#ifndef __UNMANAGEDSENDRESULTONS_H__
#define __UNMANAGEDSENDRESULTONS_H__

#include "ONSClient.h"
#include <string>

namespace ons{

class ONSCLIENT_API UnmanagedSendResultONS{
public:
    UnmanagedSendResultONS();
    virtual ~UnmanagedSendResultONS();
    void setMessageId(const std::string& msgId);
    std::string getMessageId() const;

private:
    std::string messageId;

};

}
#endif
